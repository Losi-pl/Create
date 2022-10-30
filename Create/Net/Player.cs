using Create.Conteiner;

namespace Create.Net;

public sealed class Player
{
    Account account;
    LivingEntity? entity;

    public Account Account => account;
    public LivingEntity? Entity { get => entity; set
        {
            entity?.set_player(null);
            entity = value;
            value?.set_player(this);
        }}
}
