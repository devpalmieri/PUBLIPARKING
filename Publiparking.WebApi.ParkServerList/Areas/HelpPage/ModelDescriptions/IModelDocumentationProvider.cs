using System;
using System.Reflection;

namespace Publiparking.WebApi.ParkServerList.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}