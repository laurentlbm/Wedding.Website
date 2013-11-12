using System;
using System.Web;
using System.Web.Routing;

namespace Website
{

    /// <summary>
    /// Defines a class which is used to ensure the URL parameter value is <seealso cref="Guid"/>.
    /// </summary>
    public class GuidConstraint : IRouteConstraint
    {
        private readonly bool optional;

        /// <summary>
        /// Initializes a new instance of the <see cref="GuidConstraint"/> class.
        /// </summary>
        public GuidConstraint()
            : this(false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GuidConstraint"/> class.
        /// </summary>
        /// <param name="optional">if set to <c>true</c> [optional].</param>
        public GuidConstraint(bool optional)
        {
            this.optional = optional;
        }

        /// <summary>
        /// Matches the specified HTTP context.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="route">The route.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="values">The values.</param>
        /// <param name="routeDirection">The route direction.</param>
        /// <returns></returns>
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            object value = values[parameterName];

            if (value == null)
            {
                return optional;
            }

            bool matched = false;
            Guid guid;

            if (Guid.TryParse(value.ToString(), out guid) && (guid != Guid.Empty))
            {
                matched = true;
            }

            return matched;
        }
    }
}