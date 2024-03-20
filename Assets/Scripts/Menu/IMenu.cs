namespace Menu
{
    public interface IMenu<out T>
    {
        /// <summary>
        /// Show the menu on the screen.
        /// </summary>
        void Open();
        
        /// <summary>
        /// Hide the menu from the screen.
        /// </summary>
        void Close();

        /// <summary>
        /// Get the instance associated with this menu.
        /// </summary>
        /// <returns>The not-null GameObject instance</returns>
        T GetGameObjectInstance();
    }
}