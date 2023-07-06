using Create.Conteiner;
using Create.Elements.Bazic.Entitys;

namespace Create.Net;

/// <summary>
/// Instancja połączonego gracza
/// </summary>
public sealed class Player
{
    Account account;
    LivingEntity? entity;

    public Player(Account account)
    {
        this.account = account;
    }

    /// <summary>
    /// Identyfikator gracza
    /// </summary>
    public Account Account => account;

    /// <summary>
    /// Byt połączony z graczem
    /// </summary>
    public LivingEntity? Entity { get => entity; set
        {
            if(value == null)
            {
                entity?.set_player(null);
                entity = null;
                return;
            }
            if (value?.Entity is not Mob)
                throw new Exception("The player can only possess the mob");
            entity?.set_player(null);
            entity = value;
            value?.set_player(this);
        }}
}
