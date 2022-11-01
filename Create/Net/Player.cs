using Create.Conteiner;
using Create.Elements.Bazic.Entitys;

namespace Create.Net;

public sealed class Player
{
    Account account;
    LivingEntity? entity;

    public Player(Account account)
    {
        this.account = account;
    }

    public Account Account => account;
    public LivingEntity? Entity { get => entity; set
        {
            if (value?.Entity is not Mob)
                throw new Exception("The player can only possess the mob");
            entity?.set_player(null);
            entity = value;
            value?.set_player(this);
        }}
}
