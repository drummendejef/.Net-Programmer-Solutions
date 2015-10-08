using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AspectOrientedDemo
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Method)]
    class AOPAdminRequired : PostSharp.Aspects.OnMethodBoundaryAspect
    {
        public override void OnEntry(PostSharp.Aspects.MethodExecutionArgs args)
        {
            base.OnEntry(args);

            TestForAdmin();
        }

        private void TestForAdmin()
        {
            WindowsIdentity user = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(user);
            if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
                throw new AOPNotAdminException();
        }
    }

    class AOPNotAdminException : Exception
    {
        public AOPNotAdminException() : base("The current user must be an administrator to execute this method.")
        {

        }
    }
}
