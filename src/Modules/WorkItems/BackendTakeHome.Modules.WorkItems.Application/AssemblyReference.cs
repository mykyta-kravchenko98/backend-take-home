using System.Reflection;

namespace BackendTakeHome.Modules.WorkItems.Application;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}
