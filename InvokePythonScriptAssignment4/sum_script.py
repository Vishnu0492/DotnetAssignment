# sum_script.py
import sys

def main():
    try:
        a = int(sys.argv[1])
        b = int(sys.argv[2])
        print(a + b)
    except (IndexError, ValueError) as e:
        print("Error: Please provide two integers.")

if __name__ == "__main__":
    main()