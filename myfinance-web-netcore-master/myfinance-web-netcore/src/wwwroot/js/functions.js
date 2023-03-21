function AbreRelatorio() {
    window.location.href = '../Transacao/Relatorio';
}

function RegistraTransacao() {
    window.location.href = "/Transacao/Registrar"
}

function Edit(id) {
    window.location.href = "/Transacao/Registrar/" + id
}

function Delete(id) {
    window.location.href = "/Transacao/Delete/" + id
}

function OpenModal(id) {
    $('#modalTransaction' + id).modal('show');
}

function CloseModal(id) {
    $('#modalTransaction' + id).modal('hide');
}