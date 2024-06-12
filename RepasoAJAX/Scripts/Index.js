$(document).ready(() => {
    GetAll()
})

function GetAll() {
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
                                                .on('click', (event) => {
                                                    event.preventDefault()
                                                    $('#Mensaje').modal('show')
                                                    $('.modal-content').empty().prepend(
                                                        $('<h6>').addClass('modal-header').text('¿Seguro que quieres eliminar este producto?')
                                                    )
                                                    $('.modal-content').append(
                                                        $('<div>').addClass('modal-body').append(
                                                            $('<div>').addClass('row jusitfy-content-center align-items-center').append(
                                                                $('<label>').addClass('col-form-label').text('Nombre del producto'),
                                                                $('<input type="text">').addClass('form-control').prop('disabled', true).val(value.Nombre),
                                                                $('<label>').addClass('col-form-label').text('NumMaterial'),
                                                                $('<input type="text">').addClass('form-control').prop('disabled', true).val(value.NumMateria),
                                                                $('<label>').addClass('col-form-label').text('Seccion'),
                                                                $('<input type="text">').addClass('form-control').prop('disabled', true).val(value.Categoria.Nombre),
                                                                $('<label>').addClass('col-form-label').text('Area'),
                                                                $('<input type="text">').addClass('form-control').prop('disabled', true).val(value.Categoria.Categoria.Nombre),
                                                            )
                                                        ),
                                                        $('<div>').addClass('modal-footer').append(
                                                            $('<button>').addClass('btn btn-secondary').attr("data-bs-dismiss", "modal").text('Cerrar'),
                                                            $('<button>').addClass('btn btn-success').text('Aceptar').on('click', (event) => {
                                                                $.ajax({
                                                                    url: 'https://localhost:44372/EliminarPorProducto?idProducto=' + value.Sku,
                                                                    method: 'DELETE',
                                                                    dataType: 'application/json',
                                                                    success: function (result) {
                                                                        if (result.Success) {
                                                                            GetAll()
                                                                            $('#Mensaje').modal('hide')
                                                                        }
                                                                    },
                                                                    error: {},
                                                                })
                                                            })
                                                        )
                                                    )
                                                })
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
}