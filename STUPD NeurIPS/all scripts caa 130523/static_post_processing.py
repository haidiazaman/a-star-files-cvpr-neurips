import numpy as np
import os
from tqdm import tqdm
import pandas as pd
import cv2 
import shutil

# inputs to change
num_iterations = 1000
num_frames = 90
fps=30
preposition = "all_over"
root_src = f"/Users/haidiazaman/Desktop/FYP/dataset_v4"
src = f"/Users/haidiazaman/Desktop/FYP/dataset_v4/{preposition}"

image_folder = src+"/"+[x for x in os.listdir(src) if x.startswith("RGB")][0]
image_dest_folder = src+"/"+"images"
video_dest_folder = src+"/"+preposition

# generate images for static prepositions
for j in tqdm(range(1,num_iterations+1)): #range(len(os.listdir(image_folder))) #should be 1000 for 1000 samples of single image (static prep)
    images = os.listdir(image_folder)
    images.sort(key=lambda x: int(''.join(filter(str.isdigit, x)))) #sort the image files by name, according to number
    current_file = image_folder + "/" + images[j-1]
    target_folder = image_dest_folder+"/"+str(j)
    isExist = os.path.exists(target_folder)
    if not isExist:
      # Create a new directory because it does not exist 
      os.makedirs(target_folder)
    for i in range(num_frames):
        new_img_file = target_folder+"/"+f"{i}.png"
        shutil.copy(current_file,new_img_file)
        
# generate videosss

isExist = os.path.exists(video_dest_folder)
if not isExist:
  # Create a new directory because it does not exist 
  os.makedirs(video_dest_folder)

for i in tqdm(range(1,1001)):
    current_image_folder = image_dest_folder+"/"+str(i)
    current_images = os.listdir(current_image_folder)
    current_images.sort(key=lambda x: int(''.join(filter(str.isdigit, x)))) #sort the image files by name, according to number
    img_array = []
    for file in current_images:
        full_file = current_image_folder+"/"+file
        img = cv2.imread(full_file)
        height, width, layers = img.shape
        size = (width,height)
        img_array.append(img)
    fourcc = cv2.VideoWriter_fourcc('m', 'p', '4', 'v')
    file_name=video_dest_folder+"/"+f"{i}.mp4"
    video_writer = cv2.VideoWriter(file_name,fourcc, fps, size)

    for j in range(len(img_array)):
      video_writer.write(img_array[j])
    video_writer.release()
    
    

#generate csv
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