using EntitiesLibrary.Entities.RequestFeatures;
using Microsoft.AspNetCore.Identity;

namespace EntitiesLibrary.Entities
{
    public class User : IdentityUser
    {
        public override string UserName
        {
            get => this.Email;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class UserParameters : RequestParameters
    {
        private readonly string orderByDefault = "FirstName_asc";
        public UserParameters(UserParameters parameters)
        {
            OrderBy = this.orderByDefault;

            if (parameters != null)
            {
                this.OrderBy = parameters.OrderBy;
                this.PageSize = parameters.PageSize;
                this.PageNumber = parameters.PageNumber;
                this.SearchTerm = parameters.SearchTerm;
            }
        }
        public UserParameters(string orderBy, int pageNumber, string searchTerm)
        {
            this.PageNumber = pageNumber;
            this.OrderBy = orderBy;
            this.SearchTerm = searchTerm;
        }
        public UserParameters()
        {
            this.OrderBy = this.orderByDefault;
        }
        public string SearchTerm { get; set; }
    }
}
