/// Метод преобразования массива параметров в строку для GET запроса
/// <data> - массив параметров
function encodeQueryString(data) {
    const keys = Object.keys(data)
    return keys.length
        ? "?" + keys
            .map(key => encodeURIComponent(key)
                + "=" + encodeURIComponent(data[key]))
            .join("&")
        : ""
}

/// Метод создания GET запроса
/// <url> - url для отправки запроса
/// <data> - массив параметров, при отсутствии присваивается null
async function FetchGet(url, data = null) {
    if (data != null) url = url + encodeQueryString(data);
    try {
        const response = await fetch(url,
            {
                method: 'GET',
                headers: {
                    'Accept': 'application/json; charset=utf-8',
                    'Content-Type': 'application/json;charset=UTF-8'
                }
            });
        return await response.json();
    }
    catch (ex) {
        return CreateCriticalError(ex);
    }
}

/// Метод создания POST запроса
/// <url> - url для отправки запроса
/// <data> - массив параметров, при отсутствии присваивается null
async function FetchPost(url, data = null) {
    try {
        const response = await fetch(url,
            {
                method: 'POST',
                headers: {
                    'Accept': 'application/json; charset=utf-8',
                    'Content-Type': 'application/json;charset=UTF-8'
                },
                body: JSON.stringify(data)
            }
        );
        const json = await response.json();
        return json;
    }
    catch (ex) {
        return CreateCriticalError(ex);
    }
}

/// Метод переключения видимости диалога
/// <id> - идентификатор диалога
function Toggle(currentModal) {
    if (currentModal.hasAttribute('open')) {
        currentModal.close();
    }
    else {
        currentModal.showModal();
    }
};
function CreateCriticalError(ex) {
    return {
        result: null,
        status: 2,
        errorMessage: ex.message
    }
}