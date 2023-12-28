# AI Assist Module for VirtoCommerce

## Overview
This module facilitates the generation, translation, and rephrasing of product descriptions using OpenAI. Additionally, it can be utilized for creating images for your products.


## Table of Contents
1. Setup
2. User Guide
    - Features and Usage
3. License


## Setup
To begin using this module, add the following block to the appsettings.json file in your platform:

```
"OpenAIServiceOptions": {
    "ApiKey": "your-api-key",
}
```
Make sure to replace "your-api-key" with your actual API key.

To choose text based gpt models go to Platform -> Settings -> AiAssistModule -> General

![image](https://github.com/reveation-labs/virto-openai-module/assets/109058690/e0bc3ba7-c657-4656-a3bf-6f3cefa43cd6)


## User Guide
An overview of the module's features, functionalities, and how users can leverage them within the VirtoCommerce platform.

- ### Feature : Description Generation
  Enhance your product listings with SEO-optimized descriptions. Generate compelling and unique content to captivate your audience.
* Input the following details:-
  - Specify your tailored prompt.
  - Choose the review type.
  - Indicate the desired language for your product description.
  - Define the length of the generated description.
  - Optionally, activate the setting to incorporate your product attributes within the prompt.
  - Upon clicking on Ok button, it will appear on Description Blade, where it can be saved. 

  ![image](https://github.com/reveation-labs/virto-openai-module/assets/109058690/64562745-d2bd-4811-9a56-a103a6def1f1)

---

- ### Feature : Description Translation
  Expand your market reach by effortlessly translating product descriptions into multiple languages.
* Input the following details:-
  - Select the description you want to translate.
  - Choose the language you want to translate in.
  - Click on Ok to view it in Description Blade.

  ![image](https://github.com/reveation-labs/virto-openai-module/assets/109058690/5bfc63c5-b8d9-47a4-a518-89a97ed03e0d)

---

- ### Feature : Description Rephrasing
  Ensure content uniqueness and clarity with rephrasing functionalities.
  
  ![image](https://github.com/reveation-labs/virto-openai-module/assets/109058690/313915bf-e1e4-4953-9ccc-8c0c858ea249)

---

- ### Feature : Image Generation
  Elevate your product visuals with AI-powered image generation capabilities.
  Go to product images, click on add and open generate blade. 
* Input the following details:- 
  - Provide your custom prompt to generate images
  - Choose your preferred ai model (currently Dall-e-2 and Dall-e-3 are available)
  - Choose the quality and size of images to be generated
  - Specify the number of images to generate (max allowed : Dall-e-2 = 10, Dall-e-3 = 1)
  - Select the images you want to use and click on Ok
  - The selected images will appear in the preview section of the Upload Images blade.
  - Click on Ok to save the images.

  ![image](https://github.com/reveation-labs/virto-openai-module/assets/109058690/7f49fe38-b4d1-47dc-8691-4f73b6866e2a)

  ![image](https://github.com/reveation-labs/virto-openai-module/assets/109058690/2c2d6020-b9c4-4689-992c-382d5474cc82)

  ![image](https://github.com/reveation-labs/virto-openai-module/assets/109058690/4e3f23c5-383c-4516-a789-6dd93c358274)


## License
Copyright (c) Sharpcode Solutions. All rights reserved.

Licensed under the Sharpcode Solutions Open Software License (the "License"). You may not use this file except in compliance with the License.

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

