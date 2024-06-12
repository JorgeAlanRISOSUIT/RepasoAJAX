$(document).ready(() => {
    $.ajax({
        url: "https://localhost:44372/Productos",
        contentType: 'application/json',
        type: 'GET',
        dataType: 'JSON',
        success: function (result) {
            $('#glutter').empty()
            if (result.Success) {
                $.each(result.Objects, (index, value) => {
                    $('#glutter').append(
                        $('<div>').addClass('col-md-4 my-2').append(
                            $('<div>').addClass('card').append(
                                $('<div>').addClass('container-fluid').append(
                                    $('<h6>').addClass('card-header').text(value.Nombre),
                                    $('<div>').addClass('card-body').append(
                                        $('<div>').addClass('col-sm-12 p-3').append(
                                            $('<img>').prop('alt', 'Logo')
                                        ),
                                        $('<div>').addClass('col-sm-12 d-flex justify-content-center align-items-center gap-1 flex-wrap').append(
                                            $('<a>').prop('id', 'Actualizar').prop('href', `/Producto/ObtenerProducto?idProducto=${value.Sku}`).addClass('btn btn-info col').text('Actualizar'),
                                            $('<a>').prop('id', 'Eliminar').prop('href', `/Producto/EliminarProducto?idProducto=${value.Sku}`).addClass('btn btn-danger col').text('Eliminar')
                                        )
                                    )
                                )
                            )
                        )
                    )
                })
            } else {
                $('#glutter').toggleClass('gx-3', () => !result.Success).append(
                    $('<div>').addClass('col-12 alert alert-danger').text('No existen Productos a mostrar')
                )
            }
        },
        error: function (error) {
            $('#glutter').empty()
            $('#glutter').toggleClass('gx-3', () => !result.Success).append(
                $('<div>').addClass('col-12 alert alert-danger').text('La peticion ha fallado')
            )
        }
    })
})

