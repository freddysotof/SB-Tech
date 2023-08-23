using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SB.Domain.Common.Models
{
    public class ModelStateFeature
    {
        public ModelStateDictionary ModelState { get; set; }

        public ModelStateFeature(ModelStateDictionary state)
        {
            this.ModelState = state;
        }
    }
}
