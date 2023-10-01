

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("USERS");

        builder.Property(p=> p.UserName)
        .HasColumnName("user_name")
        .HasMaxLength(50)
        .IsRequired();

        builder.Property(p=> p.Password)
        .HasColumnName("password")
        .HasMaxLength(255)
        .IsRequired();



        builder.HasOne( usr => usr.Rol)
        .WithMany( rl => rl.Users)
        .HasForeignKey( usr=> usr.IdRolFk );

        // ! [empleado-usuario] Relación de uno a uno, aparte de empleado, se añade aquí también 
        // ! para indicarle a EntityFramework que es una relación de uno a uno bidireccional.
        // ! de esta manera, aparte de que empleado acceda a los datos del usuario, usuario también podrá acceder a los datos de empleado.
        builder.HasOne(usr => usr.Empleado)
        .WithOne(empld => empld.User);





    }
}