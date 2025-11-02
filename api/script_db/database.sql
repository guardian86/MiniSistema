CREATE TABLE productos
(
    "Id" INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    "Nombre" VARCHAR(50) NOT NULL,
    "Cantidad" INTEGER NOT NULL
);

ALTER TABLE IF EXISTS productos
    OWNER TO postgres;
go

INSERT INTO productos ("Nombre", "Cantidad")
VALUES ('Café Premium', 100);

go


select * from productos;
SELECT gen_random_uuid();

SELECT * FROM productos;



