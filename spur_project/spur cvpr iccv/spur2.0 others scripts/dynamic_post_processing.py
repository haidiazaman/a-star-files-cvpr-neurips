import numpy as np
import os
from tqdm import tqdm
import pandas as pd
import cv2 

# inputs to change
preposition="down"
root_src = f"/Users/haidiazaman/Desktop/FYP/dataset_v4"
src = f"/Users/haidiazaman/Desktop/FYP/dataset_v4/{preposition}"
subfolder = "dataset_v4"
directory="/Users/haidiazaman/Desktop/FYP"
num_iterations = 3
num_frames = 90
num_images_per_video = num_frames
fps=30


###########################
####  GENERATE VIDEOS   ### 
###########################

images_path = src+"/"+[x for x in os.listdir(src) if x.startswith("RGB")][0] + "/"
video_dest_folder = src+"/"+preposition+"/"
# Check whether the specified path exists or not
isExist = os.path.exists(video_dest_folder)
if not isExist:
  # Create a new directory because it does not exist 
  os.makedirs(video_dest_folder)
  print("The new directory is created!")
img_array = []
images = os.listdir(images_path)
images = [images_path+file for file in images]
images.sort(key=lambda x: int(''.join(filter(str.isdigit, x)))) #sort the image files by name, according to number
stacks = [images[x:x+num_images_per_video] for x in range(0, len(images), num_images_per_video)]
# len(images),len(stacks)
i=1
for stack in tqdm(stacks):
  img_array = []
  for file in stack:
    img = cv2.imread(file)
    height, width, layers = img.shape
    size = (width,height)
    img_array.append(img)
  fourcc = cv2.VideoWriter_fourcc('m', 'p', '4', 'v')
  file_name=video_dest_folder+f"{i}.mp4"
  video_writer = cv2.VideoWriter(file_name,fourcc, fps, size)
  for j in range(len(img_array)):
      video_writer.write(img_array[j])
  video_writer.release()
  i+=1
print("VIDEO GENERATION DONE")

###########################
####  GENERATE CSV      ### 
###########################

df_output_path = src
df_file_name = f"{preposition}.csv"
obj_color_type_path1=f"{root_src}/colors_txt_files/colors_{preposition}1.txt"
obj_color_type_path2=f"{root_src}/colors_txt_files/colors_{preposition}2.txt"
obj_color_type1 = open(obj_color_type_path1).read().splitlines()
obj_color_type2 = open(obj_color_type_path2).read().splitlines()
dict_to_df = {
    "sense":[],
    "video_file_name":[],
    "object_1":[],
    "object_2":[],
}
for i in range(1,num_iterations+1):
    dict_to_df["sense"].append(preposition)
    dict_to_df["video_file_name"].append(f"{i}.mp4")
    dict_to_df["object_1"].append(obj_color_type1[i-1])
    dict_to_df["object_2"].append(obj_color_type2[i-1])
df=pd.DataFrame(dict_to_df)
output_path = df_output_path+"/"+df_file_name
df.to_csv(output_path)
df.head()
print("CSV FILE GENERATION DONE")