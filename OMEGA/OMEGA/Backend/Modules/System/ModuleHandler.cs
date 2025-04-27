using OMEGA.Frontend;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace OMEGA.Backend.Modules.System
{
    internal static class ModuleHandler
    {
        internal static List<Module> modules = new List<Module>();
        internal static List<Category> categories = new List<Category>();
        internal static List<Category> categoriesExcludeConfig = new List<Category>();

        public static void Awake()
        {
            modules = typeof(Module)
                .Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Module)) && !t.IsAbstract)
                .Select(t => (Module)Activator.CreateInstance(t)).ToList();

            categories = typeof(Category)
                .Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Category)) && !t.IsAbstract)
                .Select(t => (Category)Activator.CreateInstance(t)).ToList();

            foreach (Module module in modules)
                module.ModuleId = Guid.NewGuid().ToString();

            foreach (Category category in categories)
                category.CategoryId = Guid.NewGuid().ToString();

            List<Module> pinned = modules.Where(_module => _module.Pinned && _module.IsTogglable).ToList();
            List<Module> nonTogglable = modules.Where(_module => !_module.IsTogglable).ToList();
            modules = modules.Where(_module => _module.IsTogglable && !_module.Pinned).ToList();

            categories.Sort();
            modules.Sort();

            pinned.Sort();
            nonTogglable.Sort();

            List<Module> _modules = new List<Module>();

            _modules.AddRange(nonTogglable);
            _modules.AddRange(pinned);
            _modules.AddRange(modules);

            modules = _modules;
            categoriesExcludeConfig = new List<Category>();
            categoriesExcludeConfig.AddRange(categories);
            categoriesExcludeConfig.RemoveAt(categoriesExcludeConfig.Count - 1);

            if (Globals.environment != ProductEnvironment.Development) return;

            StringBuilder builder = new StringBuilder();

            foreach(Category cat in categories)
            {
                builder.AppendLine("╭─────────────────────────────────────────────────────────────────────────────────╮");
                builder.AppendLine($"│ {cat.Name.PadLeft(((78 - cat.Name.Length) / 2) + cat.Name.Length).PadRight(79)} │");
                builder.AppendLine("├─────────────────────────────────────────────────────────────────────────────────┤");

                modules.Where(_mod => _mod.Category == cat.Name).ForEach(_mod => builder.AppendLine($"│ {_mod.Name,20} │ {_mod.ToolTip,-56} │"));
                builder.AppendLine("╰─────────────────────────────────────────────────────────────────────────────────╯");
            }

            File.WriteAllText("modules.txt", builder.ToString());
        }

        public static void OnButtonCollided(string ModuleId)
        {
            if (ModuleId == null) return;

            Module module = GetModule(ModuleId);

            if (module.IsTogglable)
            {
                module.State = !module.State;
                module.OnStateChanged();
            }
            else
                module.OnStateChanged();
        }

        public static void OnCategoryButtonCollided(string categoryId)
        {
            if (categoryId == null) return;

            Category category = GetCategory(categoryId);
            if (category == null) return;

            WristMenu.currentCategory = category.Name;
            WristMenu.pageIndex = 0;
            WristMenu.RefreshButtons();
        }

        public static void Update()
        {
            foreach (Module module in modules)
                module.Update();
        }
         
        public static Category GetCategoryByIndex(int index) => categories.Where(t => t.Index == index).First();
        public static Category GetCategory(string CategoryId) => categories.Where(t => t.CategoryId == CategoryId).First();
        public static Module GetModule(string ModuleId) => modules.Where(t => t.ModuleId == ModuleId).First();
    }
}
