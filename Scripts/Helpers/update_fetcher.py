import json
import urllib.request

GITHUB_API = "https://api.github.com/repos/vava62600/keybmacro/releases/latest"

def get_latest_version():
    headers = {'User-Agent': 'KeybMacroUpdateChecker'}
    req = urllib.request.Request(GITHUB_API, headers=headers)
    with urllib.request.urlopen(req) as response:
        data = json.loads(response.read().decode())
        tag = data.get("tag_name", "0.0.0")
        if tag.startswith("v"):
            tag = tag[1:]
        return tag

def main():
    current_version = "2.0.0"
    latest_version = get_latest_version()
    has_update = latest_version > current_version
    result = {
        "current": current_version,
        "latest": latest_version,
        "has_update": has_update
    }
    print(json.dumps(result))

if __name__ == "__main__":
    main()
