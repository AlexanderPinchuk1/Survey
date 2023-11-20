
function changePage(num) {
    console.log(num);
    const res = +document.getElementById("NumPage").value;
    document.getElementById("NumPage").value = res + num;
}

export {changePage}