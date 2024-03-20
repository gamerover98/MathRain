using UnityEngine;

namespace Menu
{
    public abstract class MonoMenu : MonoBehaviour, IMenu<MonoBehaviour>
    {
        private bool firstLoad = true;

        private void OnEnable()
        {
            if (!firstLoad) Open();
        }

        private void Start()
        {
            if (!firstLoad) return;
            firstLoad = false;
            Open();
        }

        private void OnDisable() => Close();

        public abstract void Open();

        public abstract void Close();

        public MonoBehaviour GetGameObjectInstance() => this;

        public void SetActive(bool active) => gameObject.SetActive(active);
        public bool IsActive() => gameObject.activeSelf;
    }
}