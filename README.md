# OneDrive File Integrity Research

## What I Built
A C# tool to test if OneDrive messes with your files when you upload/download them.

## The Question
Does OneDrive preserve file integrity during upload and download operations?

## My Approach
- Upload different file types to OneDrive
- Download them back
- Compare SHA-256 hashes to see if anything changed
- Test with text files, images, PDFs, and empty files

## What Actually Happened
Hit a wall with Microsoft's authentication system - kept getting blocked even with different accounts and networks. Their fraud detection is really aggressive.

Built a mock OneDrive service instead to test the integrity checking logic. All files passed - hashes matched perfectly:
- Text file (100 bytes) 
- JPEG image (95KB)   
- PDF document (127KB) 
- Empty file (0 bytes) 

## Tools Used
- C# with .NET 8
- SHA-256 for file hashing
- Microsoft Graph API (attempted)
- Mock service for actual testing

## Main Challenge
Microsoft Graph API authentication kept failing with "Please retry with a different device or other authentication method" errors. Tried multiple client IDs, new accounts, different networks - all blocked.

## Solution
Created MockOneDriveService that mimics real OneDrive operations using local file system. 

## Future Ideas
- Try Google Drive API instead
- Test with larger files
- Add performance measurements
- Test network interruption scenarios

## Running the Code
```bash
git clone https://github.com/aleksandra1j/onedrive-integrity-research.git
cd onedrive-integrity-research/OneDriveIntegrityTool
dotnet run