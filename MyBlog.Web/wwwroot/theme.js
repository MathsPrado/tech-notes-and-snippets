(() => {
    const storageKey = "myblog-theme";
    const validThemes = ["light", "dark", "system"];
    const darkMedia = window.matchMedia("(prefers-color-scheme: dark)");

    function normalizeTheme(value) {
        return validThemes.includes(value) ? value : "system";
    }

    function getStoredTheme() {
        try {
            return normalizeTheme(localStorage.getItem(storageKey) ?? "system");
        } catch {
            return "system";
        }
    }

    function getResolvedTheme(theme) {
        if (theme === "system") {
            return darkMedia.matches ? "dark" : "light";
        }

        return theme;
    }

    function applyTheme(theme) {
        const normalizedTheme = normalizeTheme(theme);
        const resolvedTheme = getResolvedTheme(normalizedTheme);

        document.documentElement.setAttribute("data-theme", resolvedTheme);
        document.documentElement.setAttribute("data-theme-preference", normalizedTheme);
    }

    function syncSelect() {
        const select = document.getElementById("theme-select");
        if (!select) {
            return;
        }

        select.value = getStoredTheme();
    }

    function setTheme(theme) {
        const normalizedTheme = normalizeTheme(theme);

        try {
            localStorage.setItem(storageKey, normalizedTheme);
        } catch {
        }

        applyTheme(normalizedTheme);
        syncSelect();
    }

    function initTheme() {
        applyTheme(getStoredTheme());
        syncSelect();
    }

    darkMedia.addEventListener("change", () => {
        if (getStoredTheme() === "system") {
            applyTheme("system");
        }
    });

    window.themeManager = {
        setTheme,
        getTheme: getStoredTheme,
        initTheme
    };

    initTheme();
})();
