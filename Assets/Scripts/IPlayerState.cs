public interface IPlayerState
{
    public void Start(PlayerStateManager manager);
    public void FixUpdate(PlayerStateManager manager);
    public void Update(PlayerStateManager manager);
}