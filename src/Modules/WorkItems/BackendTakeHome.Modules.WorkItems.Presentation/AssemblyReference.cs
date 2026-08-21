using System.Reflection;

namespace BackendTakeHome.Modules.WorkItems.Presentation;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}
