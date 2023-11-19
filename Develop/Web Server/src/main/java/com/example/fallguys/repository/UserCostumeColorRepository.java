package com.example.fallguys.repository;

import com.example.fallguys.domain.CostumeColor;
import com.example.fallguys.domain.User;
import com.example.fallguys.domain.UserCostumeColor;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface UserCostumeColorRepository extends JpaRepository<UserCostumeColor, Long> {
    List<UserCostumeColor> findByUser(User user);
    Optional<UserCostumeColor> findByUserAndCostumeColor(User user, CostumeColor costumeColor);
}
