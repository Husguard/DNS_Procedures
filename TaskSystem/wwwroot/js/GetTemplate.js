/// Метод создания HTML верстки на основе шаблона и данных
/// <templateName> - название шаблона
/// <data> - JSON объект, хранящий данные
async function Render(templateName, data) {
    let selectTemplate = window.jsrender.templates[templateName];
    if (selectTemplate === undefined) {
        const newTemplate = await GetTemplate(templateName);
        selectTemplate = window.jsrender.templates(newTemplate);
    }
    return selectTemplate.render(data, true);
};
async function GetTemplate(templateName) {
    const response = await fetch(`/templates/${templateName}.html`);
    return await response.text();
};