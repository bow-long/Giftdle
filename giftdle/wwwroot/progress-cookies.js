window.giftdleProgress = {
    get: function (puzzleToken) {
        const name = "giftdle-progress-" + puzzleToken + "=";
        const cookie = document.cookie
            .split(";")
            .map(value => value.trim())
            .find(value => value.startsWith(name));

        if (!cookie) {
            return null;
        }

        try {
            return decodeURIComponent(cookie.substring(name.length));
        } catch {
            return null;
        }
    },

    set: function (puzzleToken, progress) {
        const name = "giftdle-progress-" + puzzleToken;
        const expires = new Date(Date.now() + 365 * 24 * 60 * 60 * 1000).toUTCString();
        const secure = window.location.protocol === "https:" ? "; Secure" : "";
        document.cookie = `${name}=${encodeURIComponent(progress)}; expires=${expires}; path=/; SameSite=Lax${secure}`;
    },

    clear: function (puzzleToken) {
        document.cookie = `giftdle-progress-${puzzleToken}=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/; SameSite=Lax`;
    }
};
