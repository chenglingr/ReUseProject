$(document).ready(function () {
   
    var GoodsID;
    $('#ShowTrans').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        GoodsID = button.data('whatever') // Extract info from data-* attributes
        // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
        // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
        var tel = button.data('tel')
        var modal = $(this)
        modal.find('#tel').html(tel)

    });

    $("#btnsubmit").click(function () {

        var con = $("#content").val();
        url = "/Tran/Add";
        parameter = { Goodsid: GoodsID, content: con };
        $.post(url, parameter, function (data) {
          $("#showok").html("留言成功！");
        });

    });
})