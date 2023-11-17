package com.example.fallguys.service;

import com.example.fallguys.domain.*;
import com.example.fallguys.dto.SuccessResponseDto;
import com.example.fallguys.dto.costumeColor.CostumeColorRequestDto;
import com.example.fallguys.dto.costumeColor.UserColorResponseDto;
import com.example.fallguys.dto.costumeColor.UserColorUpdateRequestDto;
import com.example.fallguys.dto.costumeColor.UserGetColorRequestDto;
import com.example.fallguys.dto.user.*;
import com.example.fallguys.exception.BaseException;
import com.example.fallguys.exception.BaseResponseCode;
import com.example.fallguys.repository.*;
import com.example.fallguys.util.JwtTokenProvider;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class UserService {
    private final PasswordEncoder passwordEncoder;
    private final JwtTokenProvider jwtTokenProvider;

    private final UserRepository userRepository;
    private final CostumeColorRepository costumeColorRepository;
    private final UserCostumeColorRepository userCostumeColorRepository;

    public Long signUp(UserSignupRequestDto userSignupRequestDto) throws BaseException {
        if(!nicknameCheck(userSignupRequestDto.getUserNickname())){
            // 닉네임 중복!
            throw new BaseException(BaseResponseCode.DUPLICATE_NICKNAME);
        }
        userSignupRequestDto.setUserPassword(passwordEncoder.encode(userSignupRequestDto.getUserPassword()));

        Long userNumber;
        User user;
        try {
            user = userRepository.save(userSignupRequestDto.toEntity());
            userNumber = user.getUserNumber();
            createUserCostumeColorStatus(user);
        } catch (Exception e) {
            System.out.println(e.getMessage());
            throw new BaseException(BaseResponseCode.FAILED_TO_SAVE_USER);
        }
        return userNumber;
    }

    public boolean nicknameCheck(String nickname) {
        boolean exitsUserCheck = userRepository.existsByUserNickname(nickname).orElseThrow(() -> new BaseException(BaseResponseCode.USER_NOT_FOUND));

        if (exitsUserCheck) {
            return false;
        }

        return true;
    }


    // 커스텀 컬러 리스트를 가져와 유저별 소유 여부 db 리스트 생성
    private void createUserCostumeColorStatus(User user){
        List<CostumeColor> colorList = new ArrayList<>(costumeColorRepository.findAll());
        CostumeColorRequestDto costumeColorRequestDto;
        for(int i = 0; i < colorList.size(); i++){

            // 기본으로 주어지는 색상 id
            if (i == 0 || i == 2 || i == 6 || i == 7 || i == 11 || i == 13) {
                costumeColorRequestDto = new CostumeColorRequestDto(user, colorList.get(i), true);
            }
            else {
                costumeColorRequestDto = new CostumeColorRequestDto(user, colorList.get(i), false);
            }

            userCostumeColorRepository.save(costumeColorRequestDto.toEntity());
        }
    }

    public User findUserById(String userId){
        return userRepository.findByUserId(userId).orElseThrow(() -> new BaseException(BaseResponseCode.USER_NOT_FOUND));
    }

    public UserLoginResponseDto login(UserLoginRequestDto userLoginRequestDto) {

        User user = findUserById(userLoginRequestDto.getUserId());
        if (!passwordEncoder.matches(userLoginRequestDto.getUserPassword(), user.getUserPassword()))
            throw new BaseException(BaseResponseCode.INVALID_PASSWORD);

        String token = jwtTokenProvider.createToken(userLoginRequestDto.getUserId());
        return new UserLoginResponseDto(HttpStatus.OK, token);
    }

    public User findUserByToken(){
        return userRepository.findByUserId(SecurityContextHolder.getContext()
                .getAuthentication().getName())
                .orElseThrow(() -> new BaseException(BaseResponseCode.USER_NOT_FOUND));
    }

    public UserResponseDto findByUser() {
        return new UserResponseDto(findUserByToken());
    }

    public List<UserColorResponseDto> findUserColorListByUser() {
        User user = findUserByToken();
        return userCostumeColorRepository.findByUser(user)
                .stream()
                .map(UserColorResponseDto::new)
                .collect(Collectors.toList());
    }

    public SuccessResponseDto updateUserCostumeColor(UserColorUpdateRequestDto userColorUpdateRequestDto) {
        User user = findUserByToken();

        user.setUserCostumeColor(userColorUpdateRequestDto.getCostumeColorNumber());
        userRepository.save(user);

        return new SuccessResponseDto(HttpStatus.OK);
    }

    public SuccessResponseDto updateGetUserCostumeColor(UserGetColorRequestDto userGetColorRequestDto) {
        User user = findUserByToken();
        user.setUserKudos(user.getUserKudos() - userGetColorRequestDto.getCostumeColorCost());
        userRepository.save(user);

        CostumeColor color = costumeColorRepository.findById(userGetColorRequestDto.getCostumeColorNumber())
                .orElseThrow(() -> new BaseException(BaseResponseCode.COSTUME_COLOR_NOT_FOUND));
        UserCostumeColor userColor = userCostumeColorRepository.findByUserAndCostumeColor(user, color)
                .orElseThrow(() -> new BaseException(BaseResponseCode.USER_COLOR_NOT_FOUND));
        userColor.setOwn(true);
        userCostumeColorRepository.save(userColor);

        return new SuccessResponseDto(HttpStatus.OK);
    }

    public SuccessResponseDto updateNickname(UserUpdateNameRequestDto userUpdateNameDto) {
        if(!nicknameCheck(userUpdateNameDto.getUserNickname())){
            // 닉네임 중복!
            throw new BaseException(BaseResponseCode.DUPLICATE_NICKNAME);
        }

        User user = findUserByToken();

        user.setUserNickname(userUpdateNameDto.getUserNickname());
        userRepository.save(user);

        return new SuccessResponseDto(HttpStatus.OK);
    }

    public SuccessResponseDto updateGetUserKudos(UserGetKudosRequestDto userGetKudosRequestDto) {
        User user = findUserByToken();
        user.setUserKudos(user.getUserKudos() + userGetKudosRequestDto.getGetKudos());
        userRepository.save(user);

        return new SuccessResponseDto(HttpStatus.OK);
    }
}
