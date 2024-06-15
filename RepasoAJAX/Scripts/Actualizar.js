$('#ActualizarForm').on('submit', (event) => {
    event.preventDefault()
    $.ajax({
        url: 'https://localhost:44372/ActualizarPorProducto',
        type: 'PUT',
        dataType: 'JSON',
        contentType: false,
        processData: false,
        data: {
            Sku: $('#Sku').val(),
            Nombre: $('#Nombre').val(),
            NumMateria: $('#NumMateria').val(),
            Categoria: {
                IdSubCategoria: parseInt($('#DDLSubCategoria').val()),
                Nombre: "",
                Categoria: {
                    IdCategoria: $('#DDLCategoria').val(),
                    Nombre: "",
                    Categorias: []
                },
                Categorias: []
            },
            Inventario: $('#Inventario').val(),
            Imagen: $('#ArchivoImg').prop('files')[0]
        },
        success: function (result) {
            if (result.Success) {
                $('#exampleModal').modal('show')
                $('#modalBody').empty()
                $('#modalBody').append($('<div>').addClass('col-auto alert alert-success text-center').text(result.Message))
            }
        },
        error: function (error) {
            console.log(error)
            $('#exampleModal').modal('show')
            $('#modalBody').empty()
            $('#modalBody').append($('<div>').addClass('col-auto alert alert-danger text-center').text(error.Message))
        }
    })
})

$('#ArchivoImg').on('change', (event) => {
    $('#imgProducto').prop('src', '')
    let imgInput = event.target.files[0]
    let file = new FileReader();
    file.onload = function (event) {
        $('#imgProducto').prop('src', event.target.result)
    }
    file.readAsDataURL(imgInput)
})
