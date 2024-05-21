using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EFTest
{
    internal class ConstructorBasedPropertyDiscoveryConvention : PropertyDiscoveryConvention
    {
        public ConstructorBasedPropertyDiscoveryConvention(ProviderConventionSetBuilderDependencies dependencies)
            : base(dependencies)
        {
        }

        public override void ProcessEntityTypeAdded(
            IConventionEntityTypeBuilder entityTypeBuilder,
            IConventionContext<IConventionEntityTypeBuilder> context)
            => Process(entityTypeBuilder);

        public override void ProcessEntityTypeBaseTypeChanged(
            IConventionEntityTypeBuilder entityTypeBuilder,
            IConventionEntityType? newBaseType,
            IConventionEntityType? oldBaseType,
            IConventionContext<IConventionEntityType> context)
        {
            if ((newBaseType == null
                 || oldBaseType != null)
                && entityTypeBuilder.Metadata.BaseType == newBaseType)
            {
                Process(entityTypeBuilder);
            }
        }

        private void Process(IConventionEntityTypeBuilder entityTypeBuilder)
        {
            ////Debugger.Launch();

            var constructorParameters = GetConstructorParameters();
            foreach (var memberInfo in GetRuntimeMembers())
            {
                if (constructorParameters.Contains(memberInfo.Name))
                {
                    entityTypeBuilder.Property(memberInfo);
                }
                ////if (Attribute.IsDefined(memberInfo, typeof(PersistAttribute), inherit: true))
                ////{
                ////    entityTypeBuilder.Property(memberInfo);
                ////}
                else if (memberInfo is PropertyInfo propertyInfo
                         && Dependencies.TypeMappingSource.FindMapping(propertyInfo) != null)
                {
                    entityTypeBuilder.Ignore(propertyInfo.Name);
                }
            }

            IEnumerable<MemberInfo> GetRuntimeMembers()
            {
                var clrType = entityTypeBuilder.Metadata.ClrType;

                ////foreach (var property in clrType.GetRuntimeProperties()
                ////             .Where(p => p.GetMethod != null && !p.GetMethod.IsStatic))
                ////{
                ////    yield return property;
                ////}

                foreach (var property in clrType.GetRuntimeFields())
                {
                    yield return property;
                }
            }

            IEnumerable<string> GetConstructorParameters()
            {
                var clrType = entityTypeBuilder.Metadata.ClrType;
                var constructor = clrType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Single();
                var parameterNames = constructor.GetParameters()
                    .Where(p => p.Name is not null)
                    .Select(p => p.Name!);
                return parameterNames;
            }
        }
    }
}
