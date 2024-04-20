use Neptuno

alter table Empleados
add Estado bit default 1;


create proc SP_update_empleado
	@IdEmpleado int,
	@Apellidos varchar(20),
	@Nombre varchar(20),
	@cargo varchar(40),
	@FechaContratacion date,
	@direccion varchar(60),
	@ciudad varchar(15),
	@codPostal varchar(10),
	@pais varchar(15),
	@sueldoBasico decimal(18,0)
as
begin
	update Empleados
	set Apellidos = @Apellidos,
	Nombre = @Nombre,
	cargo = @cargo,
	FechaContratacion = @FechaContratacion,
	direccion = @direccion,
	ciudad = @ciudad,
	codPostal = @codPostal,
	pais = @pais,
	sueldoBasico = @sueldoBasico
	where IdEmpleado = @IdEmpleado
end;


create proc SP_eliminar_empleado
	@IdEmpleado int
as
begin
	update Empleados
	set Estado = 0
	where IdEmpleado = @IdEmpleado
end;

create proc SP_listar_empleados
as
begin
	select 
	IdEmpleado,
	Nombre,
	Apellidos,
	cargo,
	FechaContratacion,
	direccion,
	ciudad,
	pais,
	codPostal,
	sueldoBasico
	from Empleados
	where Estado = 1
end;

