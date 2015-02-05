# Optical Builder

This application can be used to build optical rays' pathes through an optical system in a 2D space. This system can contain lenses (spherical), mirrors (both flat and spherical), prisms (polygonal), and balls. All objects (apart from mirrors) have configurable optical density. This project is intended to demonstrate geometrical optics to students. 

It has a bunch of issues (for example - won't work nicely if your display is set to 125% scale, has some save/load issues and so on, which I discovered some time later). I decided to release it under GPL license.

Good documentation is still a TODO, and code is quite a mess.

## Dependencies

.NET Framework 2.0 or newer, Adobe Reader 9 or newer.

## Controls

 * Behaviour of each instrument is altered by holding Ctrl, Alt and/or Shift keys.
 * Ctrl+C, Ctrl+V to copy and paste objects.
 * Ctrl+S to save scene.
 
## Configuration

 * You can specify environment density and amount of simulation steps (how many times a ray will reflect/refract) in Configuration window.
 * Optical Builder has multilanguage support. Translations can be added by translating and .obl file (in Languages folder, line by line), and loading it through the Configuration window. Right now there are three languages: English, Russian and Ukraininan. 
 
## Libraries and tools in use

This application is written in C#, and uses Adobe Reader to show Help (right now contains my term paper, and not the proper help, unfortunately), and TaoFramework for drawing. 

## Project History

Originally this program was made as a school project in [LIT](www.lit.dp.ua) (Dnipropetrovsk, Ukraine) and I wrote it without any knowledge of things like JSON, Version Control (and versioning in general), did not have strong foundation in OOP, etc.  I might rework it when I have free time. 

## License

License is GPL v2. See LICENSE
