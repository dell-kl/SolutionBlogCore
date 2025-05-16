/*
	Este disparador se usa cuando al administrador ingresa el producto 
	o actualiza el respectivo producto en la parte del producto_EsDescuento y producto_descuento
	, este trigger se encarga de setear en el campo producto_precioDescuento el respectivo descuento.
	==================================================================================================
	A nivel de codigo es necesario verificar que despues de agregar o editar escuchar si ya
	se termino de ejecutar este disparador.
*/

CREATE OR ALTER TRIGGER dbo.TGR_CalcularDescuento
ON dbo.Producto
AFTER INSERT, UPDATE
AS 
BEGIN 
	BEGIN 
		DECLARE @idProducto INT = ( SELECT TOP 1 producto_id 
		FROM Producto
		ORDER BY producto_id DESC );

		DECLARE @esDescuento TINYINT = ( SELECT TOP 1 producto_Esdescuento 
		FROM Producto
		ORDER BY producto_id DESC );
	END

	IF @esDescuento = 1
	BEGIN 
		-- obtenemos el valor del descuento.
		DECLARE @descuento INT = ( SELECT TOP 1 producto_descuento FROM Producto);
		DECLARE @precio DECIMAL(18,2) = (SELECT TOP 1 producto_precio FROM Producto);

		DECLARE @precioNuevo DECIMAL(18,2) = @precio-(@precio*@descuento)/100;
		
		-- generamos la actualizacion del producto
		UPDATE Producto
			SET producto_precioDescuento = @precioNuevo
		WHERE producto_id = @idProducto;

		PRINT 'correcto'
	END
END
