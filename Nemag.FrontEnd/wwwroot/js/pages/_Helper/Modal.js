var _modalLista = [];

function ExibirModal(htmlObject, AoExibirCallback, AoOcultarCallback) {
    var exibirBackdrop = _modalLista.length === 0;

    _modalLista.push(htmlObject);

    $(htmlObject).find('.nav-tabs a[href="#modal-tab-1"]').tab('show'); // pensar em uma maneira mais inteligente de identificar tabs

    $(htmlObject).on('show.bs.modal', function () {
        if (_modalLista.length > 1) {
            $(_modalLista[_modalLista.length - 2]).hide("fade", {}, 1);

            $('body').addClass('modal-open');
        }

        if (AoExibirCallback !== undefined && typeof AoExibirCallback === 'function')
            AoExibirCallback();

        $(htmlObject).off('show.bs.modal');
    });

    $(htmlObject).on('hidden.bs.modal', function () {
        if (_modalLista.length > 1) {
            $(_modalLista[_modalLista.length - 2]).show("fade", {}, 1);

            $('body').addClass('modal-open');
        }

        _modalLista.splice(_modalLista.length - 1, 1);

        if (AoOcultarCallback !== undefined && typeof AoOcultarCallback === 'function')
            AoOcultarCallback();

        $(htmlObject).off('hidden.bs.modal');

        if (_modalLista.length === 0) {
            $('body').removeClass('modal-open');

            $('.modal-backdrop').remove();
        }
    });

    exibirBackdrop = exibirBackdrop ? "static" : false;

    ExibirModalSolitario(htmlObject, exibirBackdrop);
}

function OcultarModal(htmlObject) {
    $(htmlObject).modal('hide');
}

function ExibirModalSolitario(htmlObject, exibirBackdrop) {
    $(htmlObject).modal({
        "backdrop": exibirBackdrop,
        "keyboard": false,
        "show": true
    });
}