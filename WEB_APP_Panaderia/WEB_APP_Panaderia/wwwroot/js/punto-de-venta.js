$(document).ready(function () {
    $("#panaderia").click(function () {
        $.ajax({
            url: '/Productos/ConsultarProductosPorCategoria?idCategoria=1', 
            method: 'GET',
            success: function (response) {
                renderProductosPanaderia(response);
            },
            error: function (error) {
                console.log("Error al obtener productos", error);
            }
        });
    });


    $("#pizzeria").click(function () {
        $.ajax({
            url: '/Productos/ConsultarProductosPorCategoria?idCategoria=2', 
            method: 'GET',
            success: function (response) {
                renderProductosPizzeria(response, 'pizzeria');
            },
            error: function (error) {
                console.log("Error al obtener productos", error);
            }
        });
    });

    function renderProductosPanaderia(productos) {
        let content = '';
        productos.forEach(function (producto) {
            content += `
                <div class="col-sm-2 col-md-6 col-lg-3 col-xl-3 pe-2">
                    <div class="product-info default-cover card">
                        <a href="javascript:void(0);" class="img-bg">
                            <img src="~/assets/img/panaderia/pos-product-05.png" alt="Products">
                            <span><i data-feather="check" class="feather-16"></i></span>
                        </a>
                        <h6 class="cat-name"><a href="javascript:void(0);">${producto.tipo}</a></h6>
                        <h6 class="product-name"><a href="javascript:void(0);">${producto.nombre}</a></h6>
                        <div class="d-flex align-items-center justify-content-between price">
                            <p>₡${producto.precioUnitario}</p>
                        </div>
                    </div>
                </div>
            `;
        });
        $(".tab_content[data-tab='panaderia'] .row").html(content);
    }

    function renderProductosPizzeria(productos, categoria) {
        let content = '';
        productos.forEach(function (producto) {
            content += `
                <div class="col-sm-2 col-md-6 col-lg-3 col-xl-3 pe-2">
                    <div class="product-info default-cover card">
                        <a href="javascript:void(0);" class="img-bg">
                            <img src="~/assets/img/pizzeria/pizza-personal.png" alt="Products">
                            <span><i data-feather="check" class="feather-16"></i></span>
                        </a>
                        <h6 class="cat-name"><a href="javascript:void(0);">${producto.tipo}</a></h6>
                        <h6 class="product-name"><a href="javascript:void(0);">${producto.nombre}</a></h6>
                        <div class="d-flex align-items-center justify-content-between price">
                            <span>${producto.descripcion}</span>
                            <p>₡${producto.precioUnitario}</p>
                        </div>
                    </div>
                </div>
            `;
        });
        $(`.tab_content[data-tab='pizzeria'] .row`).html(content);
    }


});
