CREATE DATABASE db_SistemaAutos;
GO
USE db_SistemaAutos;
GO

-- ============================================================
-- Tablas sin dependencias
-- ============================================================

CREATE TABLE [Sucursales] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(100),
	[Ciudad] NVARCHAR(100) NOT NULL,
	[Direccion] NVARCHAR(200) NOT NULL,
	[Telefono] NVARCHAR(20)
);

CREATE TABLE [Parqueaderos] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(100),
	[Direccion] NVARCHAR(200),
	[Telefono] NVARCHAR(20),
	[Capacidad] INT NOT NULL
);

CREATE TABLE [Talleres] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(100),
	[Direccion] NVARCHAR(200),
	[Telefono] NVARCHAR(20),
	[Capacidad] INT
);

CREATE TABLE [Inventarios] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Cantidad] INT,
	[Ubicacion] NVARCHAR(100),
	[ValorTotal] DECIMAL(18, 2) NOT NULL,
	[FechaActualizacion] DATETIME
);

CREATE TABLE [Auditorias] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Descripcion] NVARCHAR(255) NOT NULL,
	[FechaHora] DATETIME NOT NULL DEFAULT GETDATE(),
	[Usuario] NVARCHAR(100) NOT NULL,
	[Accion] NVARCHAR(50) NOT NULL
);

CREATE TABLE [Roles] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Estado] BIT NOT NULL

);
-- ===========================================================
-- Tablas con herencia
-- ===========================================================

CREATE TABLE [Clientes] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(100) NOT NULL,
	[Apellido] NVARCHAR(100),
	[Cedula] NVARCHAR(20) NOT NULL UNIQUE,
	[Edad] INT,
	[Correo] NVARCHAR(100) NOT NULL UNIQUE,
	[Telefono] NVARCHAR(20),
	[LicenciaConduccion] BIT,
	[PuntosFidelidad] INT,
);

CREATE TABLE [Empleados] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(100) NOT NULL,
	[Apellido] NVARCHAR(100),
	[Cedula] NVARCHAR(20) NOT NULL UNIQUE,
	[Edad] INT,
	[Correo] NVARCHAR(100) NOT NULL UNIQUE,
	[Telefono] NVARCHAR(20),
	[Cargo] NVARCHAR(50) NOT NULL,
	[Horario] NVARCHAR(50),
	[Salario] DECIMAL(18, 2) NOT NULL,
	[Bonificaciones] DECIMAL(18, 2),
	[Sucursales] INT NOT NULL REFERENCES [Sucursales](Id)
);

CREATE TABLE [Duenos] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(100) NOT NULL,
	[Apellido] NVARCHAR(100),
	[Cedula] NVARCHAR(20) NOT NULL UNIQUE,
	[Edad] INT,
	[Correo] NVARCHAR(100) NOT NULL UNIQUE,
	[Telefono] NVARCHAR(20),
	[CantidadAutos] INT,
	[Estado] BIT,
);

-- ============================================================
-- Tabla principal que depende de varias tablas (Autos)
-- ============================================================

CREATE TABLE [Autos] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Placa] NVARCHAR(20) NOT NULL UNIQUE,
	[Marca] NVARCHAR(50),
	[Año] INT,
	[Modelo] NVARCHAR(50),
	[Estado] BIT,
	[Color] NVARCHAR(20),
	[Parqueaderos] INT NOT NULL REFERENCES [Parqueaderos](Id),
	[Duenos] INT NOT NULL REFERENCES [Duenos](Id),
	[Sucursales] INT NOT NULL REFERENCES [Sucursales](Id),
	[Inventarios] INT NOT NULL REFERENCES [Inventarios](Id),
);

-- ============================================================
-- Tablas que dependen de clientes y empleados
-- ============================================================

CREATE TABLE [Ventas] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[FechaVenta] DATETIME NOT NULL,
	[PrecioVenta] DECIMAL(18, 2) NOT NULL,
	[TipoPago] NVARCHAR(50),
	[EstadoPago] BIT,
	[Clientes] INT NOT NULL REFERENCES [Clientes](Id),
	[Empleados] INT NOT NULL REFERENCES [Empleados](Id),
	[Autos] INT NOT NULL REFERENCES [Autos](Id)
);

-- ============================================================
-- Tablas que dependen de Autos
-- ============================================================

CREATE TABLE [Alquileres] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[FechaInicio] DATETIME NOT NULL,
	[FechaFin] DATETIME NOT NULL,
	[PrecioAlquiler] DECIMAL(18, 2) NOT NULL,
	[EstadoAlquiler] BIT,
	[Autos] INT NOT NULL REFERENCES [Autos](Id),
	[Clientes] INT NOT NULL REFERENCES [Clientes](Id),
	[Empleados] INT NOT NULL REFERENCES [Empleados](Id)
);

CREATE TABLE [Garantias] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[FechaInicio] DATETIME NOT NULL,
	[FechaFin] DATETIME NOT NULL,
	[Autos] INT NOT NULL REFERENCES [Autos](Id)
);

CREATE TABLE [Seguros] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Estado] BIT NOT NULL,
	[Tipo] NVARCHAR(50),
	[Cobertura] NVARCHAR(100),
	[Aseguradora] NVARCHAR(100),
	[Autos] INT NOT NULL REFERENCES [Autos](Id)
);

CREATE TABLE [Mantenimientos] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Fecha] DATETIME NOT NULL,
	[Tipo] NVARCHAR(50),
	[Descripcion] NVARCHAR(255) NOT NULL,
	[Costo] DECIMAL(18, 2) NOT NULL,
	[Autos] INT NOT NULL REFERENCES [Autos](Id),
	[Talleres] INT NOT NULL REFERENCES [Talleres](Id)
);

CREATE TABLE [Reservas] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[FechaReserva] DATETIME NOT NULL,
	[EstadoReserva] NVARCHAR(50),
	[Anticipo] DECIMAL(18, 2),
	[FechaVencimiento] DATETIME,
	[Autos] INT NOT NULL REFERENCES [Autos](Id),
	[Clientes] INT NOT NULL REFERENCES [Clientes](Id)
);

-- ============================================================
-- Tablas que dependen de Alquileres
-- ============================================================

CREATE TABLE [Devoluciones] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[FechaEntrega] DATETIME NOT NULL,
	[NivelCombustible] INT,
	[Kilometraje] INT,
	[Observaciones] NVARCHAR(255),
	[Alquileres] INT NOT NULL REFERENCES [Alquileres](Id)
);

CREATE TABLE [Contratos] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[TipoContrato] NVARCHAR(50),
	[FechaInicio] DATETIME NOT NULL,
	[FechaFin] DATETIME NOT NULL,
	[Descripcion] NVARCHAR(255) NOT NULL,
	[Alquileres] INT NOT NULL REFERENCES [Alquileres](Id)
);

-- ============================================================
-- Tablas que dependen de Ventas
-- ============================================================

CREATE TABLE [Promociones] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Descripcion] NVARCHAR(255),
	[Descuento] DECIMAL(18, 2) NOT NULL,
	[FechaInicio] DATETIME,
	[FechaFin] DATETIME,
	[Ventas] INT NOT NULL REFERENCES [Ventas](Id)
);

-- ============================================================
-- Tablas que dependen de Clientes
-- ============================================================

CREATE TABLE [Facturas] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Total] DECIMAL(18, 2) NOT NULL,
	[FechaEmision] DATETIME,
	[Estado] BIT,
	[Clientes] INT NOT NULL REFERENCES [Clientes](Id)
);

CREATE TABLE [Resenas] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Fecha] DATETIME NOT NULL,
	[Calificacion] INT NOT NULL,
	[Comentario] NVARCHAR(255),
	[TipoServicio] NVARCHAR(50),
	[Clientes] INT NOT NULL REFERENCES [Clientes](Id)
);

-- ============================================================
-- Tablas que dependen de Facturas
-- ============================================================

CREATE TABLE [DetallesFactura] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Subtotal] DECIMAL(18, 2) NOT NULL,
	[Descripcion] NVARCHAR(255),
	[TipoFactura] NVARCHAR(50) NOT NULL,
	[Facturas] INT NOT NULL REFERENCES [Facturas](Id)
);

CREATE TABLE [Pagos] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Monto] DECIMAL(18, 2) NOT NULL,
	[EstadoPago] BIT,
	[MetodoPago] NVARCHAR(50),
	[FechaPago] DATETIME NOT NULL,
	[Facturas] INT NOT NULL REFERENCES [Facturas](Id)
);

-- ============================================================
-- Tablas que dependen de Roles
-- ============================================================

CREATE TABLE [Permisos] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Descripcion] NVARCHAR(255),
	[Roles] INT NOT NULL REFERENCES [Roles](Id)
);

CREATE TABLE [Usuarios] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(100) NOT NULL,
	[Apellido] NVARCHAR(100) NOT NULL,
	[Correo] NVARCHAR(100) NOT NULL UNIQUE,
	[Contraseña] NVARCHAR(255) NOT NULL,
	[Telefono] NVARCHAR(20),
	[Roles] INT NOT NULL REFERENCES [Roles](Id)
);

-- ============================================================
-- Insercion de datos de prueba
-- ============================================================

INSERT INTO [Sucursales] (Nombre, Ciudad, Direccion, Telefono) VALUES
('Sucursal Central', 'Medell', 'Calle Principal 123', '555-0001');

INSERT INTO [Parqueaderos] (Nombre, Direccion, Telefono, Capacidad) VALUES
('Parqueadero Central', 'Avenida Central 456', '555-0002', 100);

INSERT INTO [Talleres] (Nombre, Direccion, Telefono, Capacidad) VALUES
('Taller Principal', 'Calle Taller 789', '555-0003', 50);

INSERT INTO [Inventarios] (Cantidad, Ubicacion, ValorTotal, FechaActualizacion) VALUES
(50, 'Bodega Principal', 500000.00, GETDATE());

INSERT INTO [Roles] (Nombre, Estado) VALUES
('Administrador', 1),
('Cliente', 1),
('Empleado', 1),
('Dueno', 1);

INSERT INTO [Clientes] (Nombre, Apellido, Cedula, Edad, Correo, Telefono, LicenciaConduccion, PuntosFidelidad) VALUES
('Juan', 'Perez', '12345678', 30, 'juan.perez@example.com', '555-0004', 1, 100);

INSERT INTO [Empleados] (Nombre, Apellido, Cedula, Edad, Correo, Telefono, Cargo, Horario, Salario, Bonificaciones, Sucursales) VALUES
('Maria', 'Gomez', '87654321', 28, 'maria.gomez@example.com', '555-0005', 'Vendedora', '9:00 - 18:00', 30000.00, 2000.00, 1);

INSERT INTO [Duenos] (Nombre, Apellido, Cedula, Edad, Correo, Telefono, CantidadAutos, Estado) VALUES
('Carlos', 'Lopez', '11223344', 45, 'carlos.lopez@example.com', '555-0006', 3, 1);

INSERT INTO [Autos] (Placa, Marca, Año, Modelo, Estado, Color, Parqueaderos, Duenos, Sucursales, Inventarios) VALUES
('ABC123', 'Toyota', 2020, 'Corolla', 1, 'Blanco', 1, 1, 1, 1);

INSERT INTO [Ventas] (FechaVenta, PrecioVenta, TipoPago, EstadoPago, Clientes, Empleados, Autos) VALUES
(GETDATE(), 20000.00, 'Efectivo', 1, 1, 1, 1);

INSERT INTO [Alquileres] (FechaInicio, FechaFin, PrecioAlquiler, EstadoAlquiler, Autos, Clientes, Empleados) VALUES
(GETDATE(), DATEADD(DAY, 7, GETDATE()), 500.00, 1, 1, 1, 1);

INSERT INTO [Garantias] (FechaInicio, FechaFin, Autos) VALUES
(GETDATE(), DATEADD(YEAR, 1, GETDATE()), 1);

INSERT INTO [Seguros] (Estado, Tipo, Cobertura, Aseguradora, Autos) VALUES
(1, 'Completo', 'Daños a terceros y robo', 'Seguros XYZ', 1);

INSERT INTO [Mantenimientos] (Fecha, Tipo, Descripcion, Costo, Autos, Talleres) VALUES
(GETDATE(), 'Preventivo', 'Cambio de aceite y revisión general', 150.00, 1, 1);

INSERT INTO [Reservas] (FechaReserva, EstadoReserva, Anticipo, FechaVencimiento, Autos, Clientes) VALUES
(GETDATE(), 'Confirmada', 100.00, DATEADD(DAY, 3, GETDATE()), 1, 1);

INSERT INTO [Devoluciones] (FechaEntrega, NivelCombustible, Kilometraje, Observaciones, Alquileres) VALUES
(GETDATE(), 80, 500, 'Devolución sin daños', 1);

INSERT INTO [Contratos] (TipoContrato, FechaInicio, FechaFin, Descripcion, Alquileres) VALUES
('Contrato de Alquiler', GETDATE(), DATEADD(DAY, 7, GETDATE()), 'Contrato para alquiler de vehículo', 1);

INSERT INTO [Promociones] (Descripcion, Descuento, FechaInicio, FechaFin, Ventas) VALUES
('Descuento de Verano', 10.00, GETDATE(), DATEADD(MONTH, 1, GETDATE()), 1);

INSERT INTO [Facturas] (Total, FechaEmision, Estado, Clientes) VALUES
(22000.00, GETDATE(), 1, 1);

INSERT INTO [Resenas] (Fecha, Calificacion, Comentario, TipoServicio, Clientes) VALUES
(GETDATE(), 5, 'Excelente servicio y atención', 'Venta', 1);

INSERT INTO [DetallesFactura] (Subtotal, Descripcion, TipoFactura, Facturas) VALUES
(20000.00, 'Venta de vehículo Toyota Corolla', 'Venta', 1);

INSERT INTO [Pagos] (Monto, EstadoPago, MetodoPago, FechaPago, Facturas) VALUES
(22000.00, 1, 'Efectivo', GETDATE(), 1);

INSERT INTO [Usuarios] (Nombre, Apellido, Correo, Contraseña, Telefono, Roles) VALUES
('Admin', 'User', 'admin.user@example.com', 'password123', '555-0007', 1);

