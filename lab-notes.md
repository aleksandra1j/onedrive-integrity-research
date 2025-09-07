# OneDrive File Integrity Research - Lab Notes

## Project Information
- **Researcher:** Aleksandra Jakovcheska  
- **Date:** September 2025
- **Tool:** C# Console Application with Microsoft Graph API (adapted to mock service)
- **Goal:** Test if files keep their integrity when uploaded/downloaded

## Setup Issues
- Tried Microsoft Graph API with device code flow
- Hit authentication blocks - "Please retry with a different device or other authentication method" error
- Tried multiple client IDs, new accounts, different networks - all blocked
- Had to create MockOneDriveService to keep working

## Test Files
- test-file.txt (100 bytes) - basic text
- logo.jpg (95KB) - image file  
- distributed_systems.pdf (127KB) - PDF document
- truly-empty.txt (0 bytes) - empty file

## Results
All files passed integrity check - SHA-256 hashes matched exactly:

- PDF: D6D421B5061833D6F1B84F02699D83B432B3CE942EA070C5BD4284654167BD7C
- JPG: 6CE7F8842F54FC7FC28A537B9573287BC4EF8E15270B00A55C5C771F39FE1449  
- TXT: 92B047ED090CB710919C32924A409B485DFC6B99AD635B3050F7CA7586C35494
- Empty: E3B0C44298FC1C149AFBF4C8996FB92427AE41E4649B934CA495991B7852B855

## Notes
- Mock service worked fine for testing the integrity logic
- SHA-256 calculation works across all file types
- Would need real OneDrive API to test actual cloud behavior