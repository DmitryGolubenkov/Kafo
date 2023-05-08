namespace Kafo.Server.Application.Authorization;

public interface IAccessControl
{
    /// <summary>
    /// Авторизован ли пользователь?
    /// </summary>
    bool IsAuthenticated();
    
    /// <summary>
    /// Проверяет, авторизован ли пользователь, и, если нет, кидает исключение
    /// </summary>
    void CheckIfAuthenticated();
}