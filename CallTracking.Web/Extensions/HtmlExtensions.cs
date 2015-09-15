using System.Web.Mvc;
using System.Web.Routing;

namespace CallTracking.Web.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString PostLink(
            this HtmlHelper helper,
            string linkText,
            string actionName,
            string controllerName,
            object routeValues,
            object htmlAttributes)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(actionName, controllerName, routeValues);

            var form = new TagBuilder("form");
            form.Attributes.Add("action", url);
            form.Attributes.Add("method", "post");

            // var formControls = helper.HttpMethodOverride(HttpVerbs.Delete).ToString();

            var submit = new TagBuilder("input");
            submit.Attributes.Add("type", "Submit");
            submit.Attributes.Add("value", linkText);

            if (htmlAttributes != null)
            {
                submit.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            }

            // formControls += submit.ToString();
            form.InnerHtml = submit.ToString();

            return MvcHtmlString.Create(form.ToString());
        }


    }
}