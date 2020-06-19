async function GetAllEmployees() {
    const response = await fetch("/GetAllEmployees", {
        method: "GET"
    });
    if (response.ok === true) {
        const users = await response.json();
        const result = users.result;
        let rows = document.querySelector("tbody");

        for (let i = 0; i < result.length; i++) {
            let row = rows.insertRow(i);
            for (var key in result[i]) {
                let cell = row.insertCell(key);
                cell.innerHTML = result[i][key];
            }
        }
    }
}
GetAllEmployees();