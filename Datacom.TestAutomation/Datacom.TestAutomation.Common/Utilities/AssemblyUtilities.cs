using System.Reflection;

namespace Datacom.TestAutomation.Common
{
    public static class AssemblyUtilities
    {
        public static Assembly[] GetSolutionAssemblies()
        {
            var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*TestAutomation*.dll")
                                      .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)));

            return assemblies.ToArray();
        }
    }
}
