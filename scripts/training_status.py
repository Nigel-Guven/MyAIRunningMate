import csv
import argparse
import sys

def determine_status(aerobic_te: float, anaerobic_te: float) -> str:
    """
    Classify the workout status based on Training Effect scores.
    """
    # Significant anaerobic work overrides the aerobic classification
    if anaerobic_te >= 2.0:
        return "Intensity"
        
    # Classify purely aerobic efforts
    if aerobic_te < 2.0:
        return "Recovery"  # Covers 0.0 to 1.9
    elif aerobic_te < 3.0:
        return "Base"      # Covers 2.0 to 2.9
    elif aerobic_te < 4.0:
        return "Tempo"     # Covers 3.0 to 3.9
    else:
        return "Threshold" # Covers 4.0 to 5.0

def process_csv(file_path: str):
    try:
        with open(file_path, mode='r', encoding='utf-8') as f:
            reader = csv.DictReader(f)
            
            # Print a header for the output
            print(f"{'Aerobic TE':<10} | {'Anaerobic TE':<12} | {'Status'}")
            print("-" * 40)
            
            for row in reader:
                try:
                    aerobic_te = float(row['aerobic_training_effect'])
                    anaerobic_te = float(row['anaerobic_training_effect'])
                    
                    status = determine_status(aerobic_te, anaerobic_te)
                    
                    # Format to 1 decimal place for clean alignment
                    print(f"{aerobic_te:<10.1f} | {anaerobic_te:<12.1f} | {status}")
                except ValueError:
                    print(f"Error parsing row data: {row}")
                except KeyError as e:
                    print(f"Missing expected column in CSV: {e}")
                    sys.exit(1)
                    
    except FileNotFoundError:
        print(f"Error: Could not find file at '{file_path}'")
        sys.exit(1)

if __name__ == "__main__":
    parser = argparse.ArgumentParser(description="Classify workout status using Training Effect.")
    parser.add_argument("file", help="Path to the input CSV file")
    
    args = parser.parse_args()
    process_csv(args.file)