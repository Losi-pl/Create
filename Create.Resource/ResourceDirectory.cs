using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Create.Resource;

public sealed class ResourceDirectory
{
    string name;
    ResourceDirectory? parent;
    ResourceDirectory[] subpaths;
    ResourceFile[] files;
    Resources? resources;

    internal ResourceDirectory(string name, ResourceDirectory[] subpaths, ResourceFile[] files)
    {
        this.name = name;
        this.subpaths = subpaths;
        this.files = files;
        for (int i = 0; i < subpaths.Length; i++)
            subpaths[i].parent = this;
        for (int i = 0; i < files.Length; i++)
            files[i].directory = this;
    }

    public ResourceDirectory? Parent => parent;

    public ResourceDirectory GetSubPath(string path)
    {
        if (path is "/" or "\\" or "")
            return this;

        if (path[^1] is '\\' or '/')
            path = path.Remove(path.Length - 1);
        if (path[0] is '\\' or '/')
            path = path.Substring(1);

        IEnumerable<string> directories = path.Split('\\', '/')
            .Where(f => 
            {
                if (string.IsNullOrWhiteSpace(f))
                    throw new Exception("Not valid path");
                return true;
            })
            .Where(f => 
            {
                if (f.Count(c => c is ':' or '*' or '?' or '"' or '\'' or '<' or '>' or '|') > 0) 
                    throw new Exception("Chars { : * ? \" \' < > | } are not allowed"); 
                return true; 
            });
        ResourceDirectory path_ = this;
        foreach(var p in directories)
            path_ = path_.subpaths.FirstOrDefault(p_ => p_.name == p) ?? throw new Exception($"Paht \"{path}\" doesn't exist");
        return path_;
    }
    public bool IsPathExist(string path)
    {
        IEnumerable<string> directories = path.Split('\\', '/')
            .Where(f =>
            {
                if (string.IsNullOrWhiteSpace(f))
                    throw new Exception("Not valid path");
                return true;
            })
            .Where(f =>
            {
                if (f.Count(c => c is ':' or '*' or '?' or '"' or '\'' or '<' or '>' or '|') > 0)
                    throw new Exception("Chars { : * ? \" \' < > | } are not allowed");
                return true;
            });
        ResourceDirectory? path_ = this;
        foreach (var p in directories)
        {
            path_ = path_!.subpaths.FirstOrDefault(p_ => p_.name == p);
            if (path_ == null)
                return false;
        }
        return true;
    }
    
    public IEnumerable<ResourceDirectory> SubPaths => subpaths;

    public ResourceFile GetFile(string name) => files.FirstOrDefault(f => f.Name == name) ?? throw new Exception($"File \"{name}\" doesn't exist");
    public bool IsFileExist(string name) => files.FirstOrDefault(f => f.Name == name) != null;

    public string Name => name;
    public string Path => path();
    public Resources Resources => resources!;
    public override string ToString() => path();
    string path()
    {
        if (Parent == null)
            return "/";
        return Parent.parent != null ? $"{Parent.path()}{Name}/" : $"{Name}/";
    }

    public IEnumerable<ResourceDirectory> AllSubPaths => all_directories();
    IEnumerable<ResourceDirectory> all_directories()
    {
        List<ResourceDirectory> directories = new();
        list_dir(this);
        return directories;
        void list_dir(ResourceDirectory dir)
        {
            directories.Add(dir);
            foreach (var d in dir.SubPaths)
                list_dir(d);
        }
    }
    public IEnumerable<ResourceFile> AllSubFiles => AllSubPaths.Cast(d => d.files).MargEnumerables();


    internal void set_mather_resources(Resources resor)
    {
        set_in_directories(this);
        void set_in_directories(ResourceDirectory dir)
        {
            dir.resources = resor;
            for(int i = 0; i < dir.subpaths.Length; i++)
                set_in_directories(dir.subpaths[i]);
        }
    }

    public IEnumerable<ResourceFile> Files => files;
}
