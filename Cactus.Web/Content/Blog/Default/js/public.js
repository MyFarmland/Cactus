$(function () {
    TopMenu();
})
//左上角菜单
function TopMenu() {
    $('.pure-menu-setup').bind('click', function () {
        if ($('.pure-menu-dropdown-list').is(":hidden")) {
            $('.pure-menu-dropdown-list').show();
        } else {
            $('.pure-menu-dropdown-list').hide();
        }
    });
}



