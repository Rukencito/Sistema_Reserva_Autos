CREATE DATABASE db_SistemaAutos;
GO
USE db_SistemaAutos;
GO

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


CREATE TABLE [Clientes] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[EstadoPago] BIT,
	[LicenciaConduccion] BIT,
	[PuntosFidelidad] INT,
	FOREIGN KEY (Id) REFERENCES Personas(Id)
);

CREATE TABLE [Empleados] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Cargo] NVARCHAR(50) NOT NULL,
	[Horario] NVARCHAR(50),
	[Salario] DECIMAL(18, 2) NOT NULL,
	[Bonificaciones] DECIMAL(18, 2),
	FOREIGN KEY (Id) REFERENCES Personas(Id),
	FOREIGN KEY (Id) REFERENCES Sucursales(Id)
);

CREATE TABLE [Duenos] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[CantidadAutos] INT,
	[Estado] BIT,
	FOREIGN KEY (Id) REFERENCES Personas(Id)
);

CREATE TABLE [Ventas] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[FechaVenta] DATETIME NOT NULL,
	[PrecioVenta] DECIMAL(18, 2) NOT NULL,
	[TipoPago] NVARCHAR(50),
	[EstadoPago] BIT,
	FOREIGN KEY (Id) REFERENCES Clientes(Id),
	FOREIGN KEY (Id) REFERENCES Empleados(Id)
);

CREATE TABLE [Autos] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Placa] NVARCHAR(20) NOT NULL UNIQUE,
	[Marca] NVARCHAR(50),
	[Año] INT,
	[Modelo] NVARCHAR(50),
	[Estado] BIT,
	[Color] NVARCHAR(20),
	FOREIGN KEY (Id) REFERENCES Parqueaderos(Id),
	FOREIGN KEY (Id) REFERENCES Duenos(Id),
	FOREIGN KEY (Id) REFERENCES Sucursales(Id),
	FOREIGN KEY (Id) REFERENCES Inventarios(Id),
	FOREIGN KEY (Id) REFERENCES Ventas(Id)
);

CREATE TABLE [Alquileres] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[FechaInicio] DATETIME NOT NULL,
	[FechaFin] DATETIME NOT NULL,
	[PrecioAlquiler] DECIMAL(18, 2) NOT NULL,
	[EstadoAlquiler] BIT,
	FOREIGN KEY (Id) REFERENCES Autos(Id),
	FOREIGN KEY (Id) REFERENCES Clientes(Id),
	FOREIGN KEY (Id) REFERENCES Empleados(Id)
);

CREATE TABLE [Devoluciones] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[FechaEntrega] DATETIME NOT NULL,
	[NivelCombustible] INT,
	[Kilometraje] INT,
	[Observaciones] NVARCHAR(255),
	FOREIGN KEY (Id) REFERENCES Alquileres(Id)
);

CREATE TABLE [Garantias] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[FechaInicio] DATETIME NOT NULL,
	[FechaFin] DATETIME NOT NULL,
	FOREIGN KEY (Id) REFERENCES Autos(Id)
)

CREATE TABLE [Seguros] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Estado] BIT NOT NULL,
	[Tipo] NVARCHAR(50),
	[Cobertura] NVARCHAR(100),
	[Aseguradora] NVARCHAR(100),
	FOREIGN KEY (Id) REFERENCES Autos(Id)
);

CREATE TABLE [Mantenimientos] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Fecha] DATETIME NOT NULL,
	[Tipo] NVARCHAR(50),
	[Descripcion] NVARCHAR(255) NOT NULL,
	[Costo] DECIMAL(18, 2) NOT NULL,
	FOREIGN KEY (Id) REFERENCES Autos(Id),
	FOREIGN KEY (Id) REFERENCES Talleres(Id)
);

CREATE TABLE [Reservas] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[FechaReserva] NVARCHAR(50) NOT NULL,
	[EstadoReserva] NVARCHAR(50),
	[Anticipo] DECIMAL(18, 2),
	[FechaVencimiento] DATETIME,
	FOREIGN KEY (Id) REFERENCES Autos(Id),
	FOREIGN KEY (Id) REFERENCES Clientes(Id)
);

CREATE TABLE [Promociones] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Descripcion] NVARCHAR(255),
	[Descuento] DECIMAL(18, 2) NOT NULL,
	[FechaInicio] DATETIME,
	[FechaFin] DATETIME,
	FOREIGN KEY (Id) REFERENCES Ventas(Id)
);

CREATE TABLE [Contratos] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[TipoContrato] NVARCHAR(50),
	[FechaInicio] DATETIME NOT NULL,
	[FechaFin] DATETIME NOT NULL,
	[Descripcion] NVARCHAR(255) NOT NULL,
	FOREIGN KEY (Id) REFERENCES Alquileres(Id)
);

CREATE TABLE [Facturas] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Total] DECIMAL(18, 2) NOT NULL,
	[FechaEmision] DATETIME,
	[IVA] DECIMAL(18, 2),
	[Estado] BIT,
	FOREIGN KEY (Id) REFERENCES Clientes(Id)
);

CREATE TABLE [DetallesFactura] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Subtotal] DECIMAL(18, 2) NOT NULL,
	[Descripcion] NVARCHAR(255),
	[TipoFactura] NVARCHAR(50) NOT NULL,
	FOREIGN KEY (Id) REFERENCES Facturas(Id)
);

CREATE TABLE [Pagos] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Monto] DECIMAL(18, 2) NOT NULL,
	[EstadoPago] BIT,
	[MetodoPago] NVARCHAR(50),
	[FechaPago] DATETIME NOT NULL,
	FOREIGN KEY (Id) REFERENCES Facturas(Id)
);

CREATE TABLE [Reseñas] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Fecha] DATETIME NOT NULL,
	[Calificacion] INT NOT NULL,
	[Comentario] NVARCHAR(255),
	[TipoServicio] NVARCHAR(50),
	FOREIGN KEY (Id) REFERENCES Clientes(Id)
);

CREATE TABLE [Auditorias] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Descripcion] NVARCHAR(255) NOT NULL,
	[FechaHora] DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE [Roles] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Estado] BIT NOT NULL
);

CREATE TABLE [Usuarios] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(100) NOT NULL,
	[Apellido] NVARCHAR(100) NOT NULL,
	[Correo] NVARCHAR(100) NOT NULL UNIQUE,
	[Contraseña] NVARCHAR(255) NOT NULL,
	[Telefono] NVARCHAR(20),
	FOREIGN KEY (Id) REFERENCES Roles(Id)
);

CREATE TABLE [Permisos] (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Descripcion] NVARCHAR(255),
	FOREIGN KEY (Id) REFERENCES Roles(Id)
);
