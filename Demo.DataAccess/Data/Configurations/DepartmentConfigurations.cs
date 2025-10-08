using Demo.DataAccess.Models.DepartmentModel;

namespace Demo.DataAccess.Data.Configurations
{
    public class DepartmentConfigurations : BaseEntityConfigurations<Department>
                                              ,IEntityTypeConfiguration<Department>
    {
        public new void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.Id).UseIdentityColumn();
            builder.Property(D => D.Name).HasColumnType("varchar(20)");
            builder.Property(D => D.Code).HasColumnType("varchar(20)");
            builder.HasMany(D=>D.Employees)
                   .WithOne(E=>E.Department)
                   .HasForeignKey(E => E.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull);
            base.Configure(builder);

        }
    }
}
