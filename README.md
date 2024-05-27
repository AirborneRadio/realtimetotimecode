# realtimetotimecode
A super basic way to convert realtime chapters into an FFMPEG metadata file
Based off a python script by Kyle Howells : https://ikyle.me/blog/2020/add-mp4-chapters-ffmpeg

Requires .NET 8.0 Runtime

Instructions:
1. Create a chapters.txt file using the format in the example chapters.txt file (MUST BE UTF_8)
2. Extract the original metadata using: ffmpeg -i INPUT.mp4 -f ffmetadata FFMETADATAFILE
3. Place both chapters.txt and the FFMETADATAFILE in a folder with the executable
4. Run the executable
5. Inject the output new_FFMETADATAFILE into your original file using: ffmpeg -i INPUT.mp4 -i new_FFMETADATAFILE -map_metadata 1 -codec copy OUTPUT.mp4
6. Verify the chapters work in VLC player
