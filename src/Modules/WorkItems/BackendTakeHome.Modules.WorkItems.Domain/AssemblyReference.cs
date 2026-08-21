using System.Reflection;

namespace BackendTakeHome.Modules.WorkItems.Domain;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}
