CREATE DATABASE db_SistemaAutos;
GO
USE db_SistemaAutos;
GO

-- ============================================================
-- TABLAS BASE (sin dependencias)
-- ============================================================

CREATE TABLE [Personas] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(100) NOT NULL,
	[Apellido] NVARCHAR(100),
	[Edad] INT,
	[Genero] NVARCHAR(10) NOT NULL,
	[Correo] NVARCHAR(100) NOT NULL UNIQUE,
	[Telefono] NVARCHAR(20)
);

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

CREATE TABLE [Roles] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Estado] BIT NOT NULL
);

CREATE TABLE [Auditorias] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Descripcion] NVARCHAR(255) NOT NULL,
	[FechaHora] DATETIME NOT NULL DEFAULT GETDATE(),
	[Usuario] NVARCHAR(100) NOT NULL,
	[Accion] NVARCHAR(50) NOT NULL
);

-- ============================================================
-- TABLAS QUE DEPENDEN DE PERSONAS
-- ============================================================

CREATE TABLE [Clientes] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[EstadoPago] BIT,
	[LicenciaConduccion] BIT,
	[PuntosFidelidad] INT,
	[Personas] INT NULL REFERENCES [Personas](Id)
);

CREATE TABLE [Empleados] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Cargo] NVARCHAR(50) NOT NULL,
	[Horario] NVARCHAR(50),
	[Salario] DECIMAL(18, 2) NOT NULL,
	[Bonificaciones] DECIMAL(18, 2),
	[Personas] INT NULL REFERENCES [Personas](Id),
	[Sucursales] INT NULL REFERENCES [Sucursales](Id)
);

CREATE TABLE [Duenos] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[CantidadAutos] INT,
	[Estado] BIT,
	[Personas] INT NULL REFERENCES [Personas](Id)
);

-- ============================================================
-- TABLAS QUE DEPENDEN DE CLIENTES Y EMPLEADOS
-- ============================================================

CREATE TABLE [Ventas] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[FechaVenta] DATETIME NOT NULL,
	[PrecioVenta] DECIMAL(18, 2) NOT NULL,
	[TipoPago] NVARCHAR(50),
	[EstadoPago] BIT,
	[Clientes] INT NULL REFERENCES [Clientes](Id),
	[Empleados] INT NULL REFERENCES [Empleados](Id)
);

-- ============================================================
-- TABLAS QUE DEPENDEN DE PARQUEADEROS, DUENOS, SUCURSALES, INVENTARIOS, VENTAS
-- ============================================================

CREATE TABLE [Autos] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Placa] NVARCHAR(20) NOT NULL UNIQUE,
	[Marca] NVARCHAR(50),
	[Año] INT,
	[Modelo] NVARCHAR(50),
	[Estado] BIT,
	[Color] NVARCHAR(20),
	[Parqueaderos] INT NULL REFERENCES [Parqueaderos](Id),
	[Duenos] INT NULL REFERENCES [Duenos](Id),
	[Sucursales] INT NULL REFERENCES [Sucursales](Id),
	[Inventarios] INT NULL REFERENCES [Inventarios](Id),
	[Ventas] INT NULL REFERENCES [Ventas](Id)
);

-- ============================================================
-- TABLAS QUE DEPENDEN DE AUTOS
-- ============================================================

CREATE TABLE [Alquileres] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[FechaInicio] DATETIME NOT NULL,
	[FechaFin] DATETIME NOT NULL,
	[PrecioAlquiler] DECIMAL(18, 2) NOT NULL,
	[EstadoAlquiler] BIT,
	[Autos] INT NULL REFERENCES [Autos](Id),
	[Clientes] INT NULL REFERENCES [Clientes](Id),
	[Empleados] INT NULL REFERENCES [Empleados](Id)
);

CREATE TABLE [Garantias] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[FechaInicio] DATETIME NOT NULL,
	[FechaFin] DATETIME NOT NULL,
	[Autos] INT NULL REFERENCES [Autos](Id)
);

CREATE TABLE [Seguros] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Estado] BIT NOT NULL,
	[Tipo] NVARCHAR(50),
	[Cobertura] NVARCHAR(100),
	[Aseguradora] NVARCHAR(100),
	[Autos] INT NULL REFERENCES [Autos](Id)
);

CREATE TABLE [Mantenimientos] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Fecha] DATETIME NOT NULL,
	[Tipo] NVARCHAR(50),
	[Descripcion] NVARCHAR(255) NOT NULL,
	[Costo] DECIMAL(18, 2) NOT NULL,
	[Autos] INT NULL REFERENCES [Autos](Id),
	[Talleres] INT NULL REFERENCES [Talleres](Id)
);

CREATE TABLE [Reservas] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[FechaReserva] NVARCHAR(50) NOT NULL,
	[EstadoReserva] NVARCHAR(50),
	[Anticipo] DECIMAL(18, 2),
	[FechaVencimiento] DATETIME,
	[Autos] INT NULL REFERENCES [Autos](Id),
	[Clientes] INT NULL REFERENCES [Clientes](Id)
);

-- ============================================================
-- TABLAS QUE DEPENDEN DE ALQUILERES
-- ============================================================

CREATE TABLE [Devoluciones] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[FechaEntrega] DATETIME NOT NULL,
	[NivelCombustible] INT,
	[Kilometraje] INT,
	[Observaciones] NVARCHAR(255),
	[Alquileres] INT NULL REFERENCES [Alquileres](Id)
);

CREATE TABLE [Contratos] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[TipoContrato] NVARCHAR(50),
	[FechaInicio] DATETIME NOT NULL,
	[FechaFin] DATETIME NOT NULL,
	[Descripcion] NVARCHAR(255) NOT NULL,
	[Alquileres] INT NULL REFERENCES [Alquileres](Id)
);

-- ============================================================
-- TABLAS QUE DEPENDEN DE VENTAS
-- ============================================================

CREATE TABLE [Promociones] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Descripcion] NVARCHAR(255),
	[Descuento] DECIMAL(18, 2) NOT NULL,
	[FechaInicio] DATETIME,
	[FechaFin] DATETIME,
	[Ventas] INT NULL REFERENCES [Ventas](Id)
);

-- ============================================================
-- TABLAS QUE DEPENDEN DE CLIENTES
-- ============================================================

CREATE TABLE [Facturas] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Total] DECIMAL(18, 2) NOT NULL,
	[FechaEmision] DATETIME,
	[IVA] DECIMAL(18, 2),
	[Estado] BIT,
	[Clientes] INT NULL REFERENCES [Clientes](Id)
);

CREATE TABLE [Reseñas] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Fecha] DATETIME NOT NULL,
	[Calificacion] INT NOT NULL,
	[Comentario] NVARCHAR(255),
	[TipoServicio] NVARCHAR(50),
	[Clientes] INT NULL REFERENCES [Clientes](Id)
);

-- ============================================================
-- TABLAS QUE DEPENDEN DE FACTURAS
-- ============================================================

CREATE TABLE [DetallesFactura] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Subtotal] DECIMAL(18, 2) NOT NULL,
	[Descripcion] NVARCHAR(255),
	[TipoFactura] NVARCHAR(50) NOT NULL,
	[Facturas] INT NULL REFERENCES [Facturas](Id)
);

CREATE TABLE [Pagos] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Monto] DECIMAL(18, 2) NOT NULL,
	[EstadoPago] BIT,
	[MetodoPago] NVARCHAR(50),
	[FechaPago] DATETIME NOT NULL,
	[Facturas] INT NULL REFERENCES [Facturas](Id)
);

-- ============================================================
-- TABLAS QUE DEPENDEN DE ROLES
-- ============================================================

CREATE TABLE [Usuarios] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(100) NOT NULL,
	[Apellido] NVARCHAR(100) NOT NULL,
	[Correo] NVARCHAR(100) NOT NULL UNIQUE,
	[Contraseña] NVARCHAR(255) NOT NULL,
	[Telefono] NVARCHAR(20),
	[Roles] INT NULL REFERENCES [Roles](Id)
);

CREATE TABLE [Permisos] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Descripcion] NVARCHAR(255),
	[Roles] INT NULL REFERENCES [Roles](Id)
);