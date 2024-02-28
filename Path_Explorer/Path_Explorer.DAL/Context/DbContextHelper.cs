
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Path_Explorer.DataAccessLayer.Context;
using Path_Explorer.Models.AbstractModel;
using System.Reflection;

namespace Path_Explorer.DAL.Context;

public static class DbContextHelper
{
    public static void SoftDeleteAutomaticBuilder(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            //other automated configurations left out
            if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                entityType.AddSoftDeleteQueryFilter();
            }
        }
    }

    public static void TemporalTableAutomaticBuilder(ModelBuilder builder)
    {
        // builder.Entity<Ward>().ToTable("Wards", b => b.IsTemporal());

    }
    public static void UniqueKeyAutomaticBuilder(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            #region Convert UniqueKeyAttribute on Entities to UniqueKey in DB
            var properties = entityType.GetProperties();
            if (properties != null && properties.Any())
            {
                foreach (var property in properties)
                {
                    var uniqueKeys = GetUniqueKeyAttributes(entityType, property);
                    if (uniqueKeys != null)
                    {
                        foreach (var uniqueKey in uniqueKeys.Where(x => x.Order == 0))
                        {
                            // Single column Unique Key
                            if (string.IsNullOrWhiteSpace(uniqueKey.GroupId))
                            {
                                entityType.AddIndex(property).IsUnique = true;
                            }
                            // Multiple column Unique Key
                            else
                            {
                                var mutableProperties = new List<IMutableProperty>();
                                properties.ToList().ForEach(x =>
                                {
                                    var uks = GetUniqueKeyAttributes(entityType, x);
                                    if (uks != null)
                                    {
                                        foreach (var uk in uks)
                                        {
                                            if (uk != null && uk.GroupId == uniqueKey.GroupId)
                                            {
                                                mutableProperties.Add(x);
                                            }
                                        }
                                    }
                                });
                                entityType.AddIndex(mutableProperties).IsUnique = true;
                            }
                        }
                    }
                }
            }
            #endregion Convert UniqueKeyAttribute on Entities to UniqueKey in DB
        }
    }
    private static IEnumerable<UniqueKeyAttribute> GetUniqueKeyAttributes(IMutableEntityType entityType, IMutableProperty property)
    {
        //if (entityType == null)
        //{
        //    throw new ArgumentNullException(nameof(entityType));
        //}
        //else if (entityType.ClrType == null)
        //{
        //    throw new ArgumentNullException(nameof(entityType.ClrType));
        //}
        //else if (property == null)
        //{
        //    throw new ArgumentNullException(nameof(property));
        //}
        //else if (property.Name == null)
        //{
        //    throw new ArgumentNullException(nameof(property.Name));
        //}
        var propInfo = entityType.ClrType.GetProperty(
            property.Name,
            BindingFlags.NonPublic |
            BindingFlags.Public |
            BindingFlags.Static |
            BindingFlags.Instance |
            BindingFlags.DeclaredOnly);
        if (propInfo == null)
        {
            return null;
        }
        return propInfo.GetCustomAttributes<UniqueKeyAttribute>();
    }


    
}
