using System.Web;

namespace Convergence.React.Mvc
{
    public static class RequestFlags
    {
        private const string _key = "NODE_HAS_STATE";

        public static void SetStateFlag()
        {
            HttpContext.Current.Items[_key] = true;
        }

        public static bool GetStateFlag()
        {
            return HttpContext.Current.Items[_key] != null &&
                (bool)HttpContext.Current.Items[_key];
        }
    }
}
