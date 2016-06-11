function IsValid1() {
    var error="";
    if ($("#UserName").val().trim() == "") { error += "用户名不能为空\n"; }
    if ($("#UserPwd").val().trim() == "") { error += "密码不能为空\n"; }
    var pwd1 = $("#UserPwd").val();
    var pwd2 = $("#Pwd2").val();
    if (pwd1 != pwd2) { error += "两次密码不一致"; }
  
    if (error == "") { return true; }
    else { alert(error); return false; }
}
function IsValid() {
    var pwd1 = $("#PassWord").val();
    var pwd2 = $("#inputPassword4").val();
    if (pwd1 == pwd2) { return true; }
    else { alert("两次密码不一致"); return false; }
}
function Delete(ArticleID) {
    if (confirm("确定要删除？")) {
        url = "/Article/Delete";
        parameter = { id: ArticleID };
        $.post(url, parameter, function (data) {
            alert("删除成功！");
            window.location = "/Article/MyIndexList";
        });
    }
}


