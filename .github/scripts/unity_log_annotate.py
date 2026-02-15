import sys
from pathlib import Path

log_path = Path(sys.argv[1]) if len(sys.argv) > 1 else Path("unity.log")

if not log_path.exists():
    print(f"::warning::Unity log not found: {log_path}")
    sys.exit(0)

print("::notice::Scanning Unity log for errors/warnings...")

for line in log_path.read_text(errors="ignore").splitlines():
    s = line.strip()
    if not s:
        continue
    low = s.lower()
    if "error" in low or "exception" in low:
        print(f"::error::{s}")
    elif "warning" in low:
        print(f"::warning::{s}")

print("::notice::Unity log scan complete.")


