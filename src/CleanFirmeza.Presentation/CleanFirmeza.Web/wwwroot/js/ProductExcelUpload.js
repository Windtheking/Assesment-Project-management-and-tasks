function openExcelPicker() {
    const input = document.getElementById("excelFileInput");
    if (input) input.click();
}

document.addEventListener("DOMContentLoaded", () => {

    const input = document.getElementById("excelFileInput");
    if (!input) return;

    input.addEventListener("change", async function () {

        if (!this.files.length) return;

        const file = this.files[0];

        if (!file.name.endsWith(".xlsx")) {
            alert("Solo se permiten archivos Excel (.xlsx)");
            this.value = "";
            return;
        }

        const formData = new FormData();
        formData.append("file", file);

        try {
            const response = await fetch("/Products/UploadExcel", {
                method: "POST",
                body: formData
            });

            if (!response.ok) {
                const error = await response.text();
                throw new Error(error);
            }

            const result = await response.json();
            alert(result.message);
            location.reload();

        } catch (err) {
            alert(err.message);
        }
    });
}); 