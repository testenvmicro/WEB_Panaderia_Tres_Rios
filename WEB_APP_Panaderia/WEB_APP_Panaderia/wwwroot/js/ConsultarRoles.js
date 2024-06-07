$(document).ready(function () {
    $.ajax({
        url: '/UsuariosRoles/ConsultarUsuariosRoles',
        type: 'GET',
        dataType: 'json',
        success: function (res) {
            $('.roles').append('<option value="">Roles</option>');
            $.each(res, function (key, value) {
                $('.roles').append('<option value="' + value.idRol + '">' + value.descripcion + '</option>');
            });
        }
    });
});
